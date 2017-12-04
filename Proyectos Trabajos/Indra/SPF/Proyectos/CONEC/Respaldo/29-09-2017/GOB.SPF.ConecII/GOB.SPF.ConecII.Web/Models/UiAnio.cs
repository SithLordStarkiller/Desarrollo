﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiAnio : UiEntity
    {
        public int Identificador { get; set; }
        //public int Anios { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }
    }
}