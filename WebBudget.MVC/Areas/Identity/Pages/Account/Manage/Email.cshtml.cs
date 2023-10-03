// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebBudget.Application.Email;

namespace WebBudget.MVC.Areas.Identity.Pages.Account.Manage
{
	public class EmailModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IEmailSender _emailSender;

		public EmailModel(
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public bool IsEmailConfirmed { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[TempData]
		public string StatusMessage { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[BindProperty]
		public InputModel Input { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		public class InputModel
		{
			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Required]
			[EmailAddress]
			[Display(Name = "New email")]
			public string NewEmail { get; set; }
		}

		private async Task LoadAsync(IdentityUser user)
		{
			var email = await _userManager.GetEmailAsync(user);
			Email = email;

			Input = new InputModel
			{
				NewEmail = email,
			};

			IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			await LoadAsync(user);
			return Page();
		}

		public async Task<IActionResult> OnPostChangeEmailAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var email = await _userManager.GetEmailAsync(user);
			if (Input.NewEmail != email)
			{

				var allUsers = _userManager.Users.ToList();

				if (allUsers.Any(u => u.Id != user.Id && u.Email == Input.NewEmail))
				{
					ModelState.AddModelError(string.Empty, "This email address is already in use. Choose other one.");
					return Page();
				}

				var userId = await _userManager.GetUserIdAsync(user);
				var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

				var currentUserName = user.UserName;

				var callbackUrl = Url.Page(
					"/Account/ConfirmEmailChange",
					pageHandler: null,
					values: new { area = "Identity", userId = userId, UserName = currentUserName, email = Input.NewEmail, code = code },
					protocol: Request.Scheme);
				await _emailSender.SendEmailAsync(
					Input.NewEmail,
					"Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				await SendConfirmationEmail(user.Email, callbackUrl);

				StatusMessage = "Confirmation e-mail has been sent. Please check registration e-mail";
				return RedirectToPage();
			}
			else
			{

				ModelState.AddModelError(string.Empty, "This email address is taken!");
				return Page();
			}


			StatusMessage = "Your email is unchanged.";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostSendVerificationEmailAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			if (!ModelState.IsValid)
			{
				await LoadAsync(user);
				return Page();
			}

			var userId = await _userManager.GetUserIdAsync(user);
			var email = await _userManager.GetEmailAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var callbackUrl = Url.Page(
				"/Account/ConfirmEmail",
				pageHandler: null,
				values: new { area = "Identity", userId = userId, code = code },
				protocol: Request.Scheme);
			await _emailSender.SendEmailAsync(
				email,
				"Confirm your email",
				$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
			await SendConfirmationEmail(user.Email, callbackUrl);

			StatusMessage = "Verification email sent. Please check your email.";
			return RedirectToPage();
		}


		public async Task SendConfirmationEmail(string userEmail, string callbackUrl)
		{
			var emailReceiver = userEmail;

			var email = new SendEmail(new EmailParams
			{
				HostSmtp = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				SenderName = "Radoslaw Gucwa - WebBudget",
				SenderEmail = "radoslaw.gucwa.programista@gmail.com",
				SenderEmailPassword = "quzdmkwomsfqfeau"
			});

			var subject = "E-mail change confirmation WebBudget";
			var body = $"Hello there!" +
				$"<br/>" +
				$"To change an e-mail, please confirm it by clicking in following link: <br/>" +
				$"<a href=\"{callbackUrl}\">{callbackUrl}</a>" +
				$"<br/>" +
				$"Sincerely," +
				$"<br/> Radoslaw Gucwa";

			await email.Send(subject, body, emailReceiver);
		}


	}
}
