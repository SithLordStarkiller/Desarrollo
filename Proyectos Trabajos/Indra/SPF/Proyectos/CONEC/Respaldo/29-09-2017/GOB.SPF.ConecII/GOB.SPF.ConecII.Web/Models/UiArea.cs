﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiArea : UiEntity
    {
        public int Identificador { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}