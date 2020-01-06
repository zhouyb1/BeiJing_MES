﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.DataBase.SqlServer
{
   [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : Attribute
    {
        #region Field
        private byte _precision = 18;
        public byte _scale = 5;
        #endregion

        #region Construct
        /// <summary>
        /// <para>自定义Decimal类型的精确度属性</para>
        /// </summary>
        /// <param name="precision">precision
        /// <para>精度（默认18）</para></param>
        /// <param name="scale">scale
        /// <para>小数位数（默认5）</para></param>
        public DecimalPrecisionAttribute(byte precision = 18, byte scale = 6)
        {
            Precision = precision;
            Scale = scale;
        }
        #endregion

        #region Property
        /// <summary>
        /// 精确度（默认18）
        /// </summary>
        public byte Precision
        {
            get { return this._precision; }
            set { this._precision = value; }
        }

        /// <summary>
        /// 保留位数（默认5）
        /// </summary>
        public byte Scale
        {
            get { return this._scale; }
            set { this._scale = value; }
        }
        #endregion
    }
}