﻿namespace Domain.Entites
{
    public class ProductDetail
    {
        public int ProductDetailId { get; set; }
        public string Title { get; set; }
        public List<string> Pictures { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public Guid ProductId { get; set; }

    }
}