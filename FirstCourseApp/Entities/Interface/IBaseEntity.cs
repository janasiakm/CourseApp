﻿namespace FirstCourseApp.Entities
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
    }
}
