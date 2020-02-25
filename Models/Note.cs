﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetNote.MVC6.Models
{
    public class Note
    {
        /// <summary>
        /// 게시물 번호
        /// </summary>
        [Key]
        public int NoteNo { get; set; }

        /// <summary>
        /// 게시물 제목
        /// </summary>
        [Required(ErrorMessage = "Enter the Title.")]
        public string NoteTitle { get; set; }

        /// <summary>
        /// 게시물 내용
        /// </summary>
        [Required(ErrorMessage = "Enter the Content.")]
        public string NoteContent { get; set; }

        /// <summary>
        /// 작성자
        /// </summary>
        [Required]
        public int UserNo { get; set; }

        [ForeignKey("UserNo")]
        public virtual User User { get; set; }
    }
}
