﻿using System;

namespace ApiSite.Models {
    public class VerifyReturn {
        #region Public Properties

        public string Name { get; set; }
        public bool Success { get; set; }
        public string To { get; set; }
        public string Guid { get; set; }

        #endregion Public Properties
    }
}