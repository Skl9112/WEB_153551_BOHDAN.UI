using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace WEB_153551_BOHDAN.UI.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string NormalizedName { get; set; } = "";

        [JsonIgnore] 
        public List<Dish> Dishes { get; set; } = new();
    }
}
