﻿using ReelStream.core.External.Models;
using ReelStream.data.Models.Entities;
using ReelStream.data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Models.DataTransfer.Response
{
    public class MovieSearchResponse
    {
        public int? ExternalId { get; set; }
        
        public string Title { get; set; }
        
        public string PosterPath { get; set; }
        
        public ICollection<GenreResponse> Genres { get; set; }
        
        public bool? Adult { get; set; }
        
        public string Overview { get; set; }
        
        public DateTime? ReleaseDate { get; set; }


        public static MovieSearchResponse MapFromEntity(ExternalMovie externalMovie, IGenreRepository genreRepository)
        {
            var response = new MovieSearchResponse()
            {
                ExternalId = externalMovie.ExternalId,
                Title = externalMovie.Title,
                PosterPath = externalMovie.PosterPath,
                Adult = externalMovie.Adult,
                Overview = externalMovie.Overview,
                ReleaseDate = externalMovie.ReleaseDate
            };

            var genres = genreRepository.GetByExternalIds(externalMovie.GenreIds);
            response.Genres = new List<GenreResponse>();
            foreach (var genre in genres)
            {
                response.Genres.Add(GenreResponse.MapFromObject(genre));
            }

            return response;
        }
    }
}
