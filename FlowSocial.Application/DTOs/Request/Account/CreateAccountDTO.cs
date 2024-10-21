using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowSocial.Application.DTOs.Request.Account
{
    public class CreateAccountDTO:LoginDTO
    {
        public string Name {  get; set; }
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm  Password")]
        public string ConfirmPassword {  get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string Role {  get; set; }
        public string? Bio { get; set; }
        

    }
}
