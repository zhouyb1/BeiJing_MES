using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:49
    /// 描 述：温湿度采集参数设置
    /// </summary>
    public partial class Mes_HumitureEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 类型，温度，湿度
        /// </summary>
        [Column("H_KIND")]
        public string H_Kind { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [Column("H_IP")]
        public string H_IP { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [Column("H_PORT")]
        public string H_Port { get; set; }
        /// <summary>
        /// 上限值
        /// </summary>
        [Column("H_UP")]
        [DecimalPrecision(18, 6)]
        public decimal? H_Up { get; set; }
        /// <summary>
        /// 下限值
        /// </summary>
        [Column("H_DOWN")]
        [DecimalPrecision(18, 6)]
        public decimal? H_Down { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Column("H_ADDRESS")]
        public string H_Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("H_REMARK")]
        public string H_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

