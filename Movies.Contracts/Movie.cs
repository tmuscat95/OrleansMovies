using System;
using System.Collections.Generic;

namespace Movies.Contracts
{
	public class Movie : ICloneable
	{
		public int Id { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Genres { get; set; }
		public string Rate { get; set; }
		public string Length { get; set; }
		public string Img { get; set; }

		public Movie(int id, string key, string name, string description, List<string> genres, string rate, string length, string img)
		{
			Id = id;
			Key = key;
			Name = name;
			Description = description;
			Genres = genres;
			Rate = rate;
			Length = length;
			Img = img;
		}

		public override bool Equals(Object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else { 
				Movie m = (Movie)obj;
				return m.Name == Name && m.Description == Description && m.Id == Id && m.Key == Key && m.Rate == Rate && m.Length == Length && m.Img == Img && m.Genres.Count == Genres.Count;
			}
		}
		public Movie Update(Movie newData)
		{
			this.Key = newData.Key;
			this.Name = newData.Name;
			this.Description = newData.Description;
			this.Genres = newData.Genres;
			this.Rate = newData.Rate;
			this.Length = newData.Length;
			this.Img = newData.Img;
			return this;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
