using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RafaelaColabora.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using RafaelaColabora.Services;

namespace RafaelaColabora.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
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
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Rafaela Colabora | Confirm your email",
                    $"<p>Hi {user.FirstName}! Trust this mail finds you and beloved ones health and safe! </p>" +
                    $"<p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p>" +
                    $"<p>Thank you and have a great week!</p>" +
                    $"<p>Regards, Rafaela Colabora Team.</p>");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage();
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
                "Rafaela Colabora | Confirm your email",
                GetHtmlTemplateForEmail(user.FirstName, HtmlEncoder.Default.Encode(callbackUrl)));

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }

        public string GetHtmlTemplateForEmail(string name, string link)
        {
            string some =  @"
                    <html>

                    

<body style = ""background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;"">
 
     <!--HIDDEN PREHEADER TEXT -->
 
     <div style = ""display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;""> We're thrilled to have you here! Get ready to dive into your new account. </div>
         <table border = ""0"" cellpadding = ""0"" cellspacing = ""0"" width = ""100%"">
            
                    <!--LOGO-->
            
                    <tr>
            
                        <td bgcolor = ""#FFA73B"" align = ""center"">
               
                               <table border = ""0"" cellpadding = ""0"" cellspacing = ""0"" width = ""100%"" style = ""max-width: 600px;"">
                        
                                            <tr>
                        
                                                <td align = ""center"" valign = ""top"" style = ""padding: 40px 10px 40px 10px;""> </td>
                             
                                                 </tr>
                             
                                             </table>
                             
                                         </td>
                             
                                     </tr>
                             
                                     <tr>
                             
                                         <td bgcolor = ""#FFA73B"" align = ""center"" style = ""padding: 0px 10px 0px 10px;"">
                                  
                                                  <table border = ""0"" cellpadding = ""0"" cellspacing = ""0"" width = ""100%"" style = ""max-width: 600px;"">
                                           
                                                               <tr>
                                           
                                                                   <td bgcolor = ""#ffffff"" align = ""center"" valign = ""top"" style = ""padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;"">
                                                  
                                                                              <h1 style = ""font-size: 48px; font-weight: 400; margin: 2;""> Welcome " + name + @"! </h1> <img src = "" https://www.google.com/url?sa=i&url=https%3A%2F%2Fwpguynews.com%2Fhow-to-fix-the-ssl-handshake-failed-error%2F&psig=AOvVaw3hmRD0F0L8wE2xjXiqeUGY&ust=1629897355045000&source=images&cd=vfe&ved=0CAsQjRxqFwoTCPiVp8zfyfICFQAAAAAdAAAAABAD"" width = ""125"" height = ""120"" style = ""display: block; border: 0px;"" />
                                                                 
                                                                                         </td>
                                                                 
                                                                                     </tr>
                                                                 
                                                                                 </table>
                                                                 
                                                                             </td>
                                                                 
                                                                         </tr>
                                                                 
                                                                         <tr>
                                                                 
                                                                             <td bgcolor = ""#f4f4f4"" align = ""center"" style = ""padding: 0px 10px 0px 10px;"">
                                                                      
                                                                                      <table border = ""0"" cellpadding = ""0"" cellspacing = ""0"" width = ""100%"" style = ""max-width: 600px;"">
                                                                               
                                                                                                   <tr>
                                                                               
                                                                                                       <td bgcolor = ""#ffffff"" align = ""left"" style = ""padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                                                                    
                                                                                                                <p style = ""margin: 0;""> We're excited to have you get started. First, you need to confirm your account. Just press the button below.</p>
                                                                                                                 </td>
                                                                                         
                                                                                                             </tr>
                                                                                         
                                                                                                             <tr>
                                                                                         
                                                                                                                 <td bgcolor = ""#ffffff"" align = ""left"">
                                                                                            
                                                                                                                        <table width = ""100%"" border = ""0"" cellspacing = ""0"" cellpadding = ""0"">
                                                                                                   
                                                                                                                                   <tr>
                                                                                                   
                                                                                                                                       <td bgcolor = ""#ffffff"" align = ""center"" style = ""padding: 20px 30px 60px 30px;"">
                                                                                                        
                                                                                                                                                <table border = ""0"" cellspacing = ""0"" cellpadding = ""0"">
                                                                                                             
                                                                                                                                                         <tr>
                                                                                                             
                                                                                                                                                             <td align = ""center"" style = ""border-radius: 3px;"" bgcolor = ""#FFA73B""><a href = "" " + link + @" "" target = ""_blank"" style = ""font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #FFA73B; display: inline-block;""> Confirm Account </a></td>
                                                                                                                              
                                                                                                                                                                          </tr>
                                                                                                                              
                                                                                                                                                                      </table>
                                                                                                                              
                                                                                                                                                                  </td>
                                                                                                                              
                                                                                                                                                              </tr>
                                                                                                                              
                                                                                                                                                          </table>
                                                                                                                              
                                                                                                                                                      </td>
                                                                                                                              
                                                                                                                                                  </tr> <!--COPY-->
                                                                                                                              
                                                                                                                                                  <tr>
                                                                                                                              
                                                                                                                                                      <td bgcolor = ""#ffffff"" align = ""left"" style = ""padding: 0px 30px 0px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                                                                                                                   
                                                                                                                                                               <p style = ""margin: 0;""> If that doesn't work, copy and paste the following link in your browser:</p>
                                                                                                                                                                </td>
                                                                                                                                        
                                                                                                                                                            </tr> <!--COPY-->
                                                                                                                                        
                                                                                                                                                            <tr>
                                                                                                                                        
                                                                                                                                                                <td bgcolor = ""#ffffff"" align = ""left"" style = ""padding: 20px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                                                                                                                             
                                                                                                                                                                         <p style = ""margin: 0;""><a href = ""#"" target = ""_blank"" style = ""color: #FFA73B;""> " + link + @" </a></p>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor = ""#ffffff"" align = ""left"" style = ""padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
     
                                 <p style = ""margin: 0;""> If you have any questions, just reply to this email—we're always happy to help out.</p>
                                  </td>
          
                              </tr>
          
                              <tr>
          
                                  <td bgcolor = ""#ffffff"" align = ""left"" style = ""padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
               
                                           <p style = ""margin: 0;""> Cheers,<br> BBB Team </p>
                       
                                               </td>
                       
                                           </tr>
                       
                                       </table>
                       
                                   </td>
                       
                               </tr>
                       
                               <tr>
                       
                                   <td bgcolor = ""#f4f4f4"" align = ""center"" style = ""padding: 30px 10px 0px 10px;"">
                            
                                        <table border = ""0"" cellpadding = ""0"" cellspacing = ""0"" width = ""100%"" style = ""max-width: 600px;"">
                                     
                                            <tr>
                                     
                                                <td bgcolor = ""#FFECD1"" align = ""center"" style = ""padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                          
                                                        <h2 style = ""font-size: 20px; font-weight: 400; color: #111111; margin: 0;""> Need more help?</h2>
                                               
                                                            <p style = ""margin: 0;""><a href = ""#"" target = ""_blank"" style = ""color: #FFA73B;""> We are here to help you out</a></p>
                                                               
                                                </td>
                                                               
                                            </tr>
                                                               
                                        </table>
                                                               
                                    </td>
                                                               
                                </tr>
                                                               
                                                                                            
                            </table>

                        </body>

                     </html> ";

            string someelse = @"
                <html>
                    <head>
                        <title></title>
                        <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1""
                        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge""/>
                        <style>
                            @import url('https://fonts.googleapis.com/css?family=Open+Sans');

                            * {
                                box-sizing: border-box;
                            }

                            a {
                                color:rgba(0, 0, 0);
                                text-decoration: none;
                            }

                            .c-email {
                                width: 50vw;
                                border-radius: 40px;
                                overflow: hidden;
                                box-shadow: 0px 7px 22px 0px rgba(0, 0, 0, .1);
                            }

                            .c-email__header,
                            .c-email__footer {
                                background-color: #0fd59f;
                                width: 100%;
                                height: 60px;
                            }

                            .c-email__header__title,
                            .c-email__footer__title {
                                font-size: 23px;
                                font-family: 'Open Sans';
                                height: 60px;
                                line-height: 60px;
                                margin: 0;
                                text-align: center;
                                color: white;
                            }

                            .c-email__content {
                                width: 100%;
                                height: auto;
                                background-color: #fff;
                                padding: 15px;
                            }

                            .c-email__content__text {
                                font-size: 20px;
                                text-align: center;
                                color: #343434;
                                margin-top: 0;
                            }

                            .c-email__code {
                                cursor: pointer;
                                border: none;
                                display: block;
                                margin: 30px auto;
                                background-color: #ddd;
                                border-radius: 40px;
                                padding: 20px;
                                text-align: center;
                                font-size: 36px;
                                font-family: 'Open Sans';
                                letter-spacing: 10px;
                                box-shadow: 0px 7px 22px 0px rgba(0, 0, 0, .1);
                            }

                            .text-title {
                                font-family: 'Open Sans';
                            }

                            .text-center {
                                text-align: center;
                            }

                            .text-italic {
                                font-style: italic;
                            }

                            .opacity-30 {
                                opacity: 0.3;
                            }

                            .mb-0 {
                                margin-bottom: 0;
                            }
                        </style>
                    </head>
                    <body>
                        <table align=""center"" style=""margin: auto;"">
                            <tr>
                                <td>
                                    <div class=""c-email"">
                                        <div class=""c-email__header"">
                                            <h1 class=""c-email__header__title"">Hi " + name + @"!</h1>
                                        </div>
                                        <div class=""c-email__content"">
                                            <p class=""c-email__content__text text-title"">
                                                We're excited to have you get started! <br> First, you need to confirm your account. Just press the button below:
                                            </p>
                                            <a href=""" + link + @""" target=""_blank"">
                                                <div class=""c-email__code"">
                                                    <span class=""c-email__code__text"">Confirm Account</span>
                                                </div>
                                            </a>
                                            <p class=""c-email__content__text text-italic opacity-30 text-title mb-0"">
                                                If that doesn't work, copy and paste the following link in your browser:
                                            </p>
                                            <div style=""text-align: center;""><a href=""" + link + @""" target=""_blank"" style=""color: #0fd59f;""><small>" + link + @"</small></a></div>
                                            <p class=""c-email__content__text text-italic opacity-30 text-title mb-0"">Verification code is valid only for
                                                1 day
                                            </p>
                                            <p class=""c-email__content__text text-italic opacity-30 text-title mb-0"">
                                                If you have any questions, just reply to this email — we're always happy to help out!
                                            </p>
                                        </div>
                                        <div class=""c-email__footer"">
                                            <h1 class=""c-email__footer__title"">Cheers, Rafaela Colabora Team</h1>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </body>
                </html>
                ";
            return someelse;
        }
    }
}
