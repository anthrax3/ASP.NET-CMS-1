using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Project Name"), DataType(DataType.Text, ErrorMessage = "Project name is invalid!")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Description"), DataType(DataType.Text, ErrorMessage = "Project description is invalid!")]
        public string ProjectDesc { get; set; }
        [Display(Name = "Project Image"), DataType(DataType.ImageUrl, ErrorMessage = "Project image is invalid!")]
        public string ProjectImage { get; set; }
        [Display(Name = "Post Date"), DataType(DataType.DateTime, ErrorMessage = "Project date is invalid!")]
        public DateTime PostedOn { get; set; }
    }
}