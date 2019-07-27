using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models
{
    /// <summary>
    /// Переговорная
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Количество кресел
        /// </summary>
        [DisplayName("Количество кресел")]
        public int SeatsCount { get; set; }

        /// <summary>
        /// Наличие проектора
        /// </summary>
        [DisplayName("Есть проектор")]
        public bool IsProjector { get; set; }

        /// <summary>
        /// Наличие маркерной доски
        /// </summary>
        [DisplayName("Есть маркерная доска")]
        public bool IsMarkerBoard { get; set; }

        /// <summary>
        /// Комната недоступна
        /// </summary>
        [DisplayName("Заблокировать переговорку")]
        public bool IsBlock { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}