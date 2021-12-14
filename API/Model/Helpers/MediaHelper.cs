using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.DTOs;
using API.Model.Entities;

namespace API.Model.Helpers
{
    public static class MediaHelper
    {
        public static IEnumerable<Media> ConvertMediaDtoToMedia(List<MediaDTO> mediaDTOs){
            foreach(MediaDTO mediaDTO in mediaDTOs){
                yield return new Media{
                    Id =mediaDTO.Id, 
                    Adult = mediaDTO.Adult,
                    GenreIds = mediaDTO.GenreIds,
                    MediaType = mediaDTO.MediaType,
                    Language = mediaDTO.Language,
                    Title = mediaDTO.Title,
                    Overview = mediaDTO.Overview
                };
            }
        }
    }
}