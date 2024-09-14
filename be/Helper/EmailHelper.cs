﻿namespace be.Helper
{
    public class EmailHelper
    {
        private static EmailHelper instance;
        public static EmailHelper Instance
        {
            get { if (instance == null) instance = new EmailHelper(); return EmailHelper.instance; }
            private set { EmailHelper.instance = value; }
        }
        //sai 
        public string BodyRegisterMail(string fullname, string? email, string password)
        {
            return $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""utf-8"">
                        <meta http-equiv=""x-ua-compatible"" content=""ie=edge"">
                        <title>Email Confirmation</title>
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                        <style type=""text/css"">
                            @media screen {{
                                @font-face {{
                                    font-family: 'Source Sans Pro';
                                    font-style: normal;
                                    font-weight: 400;
                                    src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
                                }}

                                @font-face {{
                                    font-family: 'Source Sans Pro';
                                    font-style: normal;
                                    font-weight: 700;
                                    src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
                                }}
                            }}

                            body,
                            table,
                            td,
                            a {{
                                -ms-text-size-adjust: 100%;
                                -webkit-text-size-adjust: 100%;
                            }}

                            table,
                            td {{
                                mso-table-rspace: 0pt;
                                mso-table-lspace: 0pt;
                            }}

                            img {{
                                -ms-interpolation-mode: bicubic;
                            }}

                            a[x-apple-data-detectors] {{
                                font-family: inherit !important;
                                font-size: inherit !important;
                                font-weight: inherit !important;
                                line-height: inherit !important;
                                color: inherit !important;
                                text-decoration: none !important;
                            }}

                            div[style*=""margin: 16px 0;""] {{
                                margin: 0 !important;
                            }}

                            body {{
                                width: 100% !important;
                                height: 100% !important;
                                padding: 0 !important;
                                margin: 0 !important;
                                background-color: #e9ecef;
                            }}

                            table {{
                                border-collapse: collapse !important;
                            }}

                            button {{
                                background-color: #1a82e2;
                                cursor: pointer;
                            }}

                            button:hover {{
                                background-color: #06294983;
                            }}

                            img {{
                                height: auto;
                                line-height: 100%;
                                text-decoration: none;
                                border: 0;
                                outline: none;
                            }}
                        </style>

                    </head>

                    <body>
                        <div class=""preheader"" style=""display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;"">
                        </div>
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                            <tr>
                                <td align=""center"" bgcolor=""#e9ecef"">
                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                                        <tr>
                                            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 36px 24px 0; border-top: 3px solid #d4dadf;"">
                                                <h1 style=""margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;text-align: center;"">ACCOUNT CONFIRM</h1>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align=""center"" bgcolor=""#e9ecef"">
                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                                        <tr>
                                            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-size: 16px; line-height: 24px;"">
                                                <p style=""margin: 0;"">Dear {fullname}, <br />
                                                Chúng tôi nhận được yêu cầu đăng ký của bạn, đây là thông tin tài khoản và mật khẩu của bạn:<br /><br />
                                                Tài khoản: <strong>{email}</strong><br />
                                                Mật khẩu: <strong>{password}</strong><br />
                                                Không chia sẻ mật khẩu với bất kỳ ai. Vui lòng đổi mật khẩu sau khi nhận được email này. <br /><br />
                                                Kích hoạt tài khoản: <a href=https://localhost:7207/api/home/confirm?email={email} target=""_blank""
                                                    style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px;text-decoration: none; border-radius: 6px;"">Nhấn vào đây</a><br/>
                                                Chúc bạn có trải nghiệm tốt.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align=""left"" bgcolor=""#ffffff"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                    <tr>
                                                        <td align=""center"" bgcolor=""#ffffff"" style=""padding: 12px;"">
                                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                                <tr>
                                                                    <td align=""center"" bgcolor=""#1a82e2"" style=""border-radius: 6px;"">
                                                    
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align=""left"" bgcolor=""#ffffff""
                                                style=""padding: 24px; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf"">
                                                <p style=""margin: 0;""><br></p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>
                    </html>";
        }

        public string BodyForgotMail(string fullname, string? email, string password)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <meta http-equiv='x-ua-compatible' content='ie=edge'>
                    <title>Forgot Password</title>
                    <meta name='viewport' content='width=device-width, initial-scale=1'>
                    <style type='text/css'>
                        @media screen {{
                            @font-face {{
                                font-family: 'Source Sans Pro';
                                font-style: normal;
                                font-weight: 400;
                                src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
                            }}

                            @font-face {{
                                font-family: 'Source Sans Pro';
                                font-style: normal;
                                font-weight: 700;
                                src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
                            }}
                        }}

                        body,
                        table,
                        td,
                        a {{
                            -ms-text-size-adjust: 100%;
                            -webkit-text-size-adjust: 100%;
                        }}

                        table,
                        td {{
                            mso-table-rspace: 0pt;
                            mso-table-lspace: 0pt;
                        }}

                        img {{
                            -ms-interpolation-mode: bicubic;
                        }}

                        a[x-apple-data-detectors] {{
                            font-family: inherit !important;
                            font-size: inherit !important;
                            font-weight: inherit !important;
                            line-height: inherit !important;
                            color: inherit !important;
                            text-decoration: none !important;
                        }}

                        div[style*='margin: 16px 0;'] {{
                            margin: 0 !important;
                        }}

                        body {{
                            width: 100% !important;
                            height: 100% !important;
                            padding: 0 !important;
                            margin: 0 !important;
                            font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                        }}

                        table {{
                            border-collapse: collapse !important;
                        }}

                        a {{
                            color: #1a82e2;
                        }}

                        img {{
                            height: auto;
                            line-height: 100%;
                            text-decoration: none;
                            border: 0;
                            outline: none;
                        }}

                        .button {{
                            display: inline-block;
                            font-size: 16px;
                            color: #ffffff;
                            text-decoration: none;
                            background-color: #1a82e2;
                            padding: 10px 20px;
                            border-radius: 5px;
                            border: none;
                            cursor: pointer;
                            transition: background-color 0.3s ease;
                        }}

                        .button:hover {{
                            background-color: #0056b3;
                        }}

                        .preheader {{
                            display: none;
                            max-width: 0;
                            max-height: 0;
                            overflow: hidden;
                            font-size: 1px;
                            line-height: 1px;
                            color: #fff;
                            opacity: 0;
                        }}
                    </style>
                </head>

                <body style='background-color: #e9ecef;'>
                    <div class='preheader'>
                        This is a preheader text.
                    </div>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                        <tr>
                            <td align='center' bgcolor='#e9ecef'>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
                                    <tr>
                                        <td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; border-top: 3px solid #d4dadf;'>
                                            <h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px; text-align: center;'>
                                                FORGOT PASSWORD
                                            </h1>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align='center' bgcolor='#e9ecef'>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
                                    <tr>
                                        <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
                                            <p style='margin: 0;'>Dear {fullname}, <br />
                                                Chúng tôi đã nhận được yêu cầu mật khẩu mới từ bạn. Hãy sử dụng mật khẩu dưới đây để đăng nhập:
                                                <br/><br/>
                                                Mật khẩu mới: <strong>{password}</strong><br /><br />
                                                Không chia sẻ mật khẩu này với bất kỳ ai. Bạn nên đổi mật khẩu sau khi nhận được mật khẩu này.
                                                <br/><br/>
                                                Chúc bạn có trải nghiệm tốt!
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf;'>
                                            <p style='margin: 0;'><br></p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>";
        }
    }

}
