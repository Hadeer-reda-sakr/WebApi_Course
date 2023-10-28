﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Task.Models;

public partial class Department
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string name { get; set; }

    [StringLength(50)]
    public string manager { get; set; }

    [InverseProperty("Dept")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [InverseProperty("Dept")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("Dept")]
    public virtual ICollection<Traniee> Traniees { get; set; } = new List<Traniee>();
}