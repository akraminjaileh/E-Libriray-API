﻿namespace E_Libriray_API.DTOs
{
    public class CreateBookDTO
    {
        public int BookID { get; set; }
        public string ? Booktitel { get; set; }
        public float ? price { get; set; }
        public string ? description { get; set; }
    }
}
