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
        public static IEnumerable<Media> ConvertListOfMediaDtoToMedia(List<MediaDTO> mediaDTOs){
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

        public static IEnumerable<MediaDTO> ConvertListOfMediaToMediaDto(List<Media> listOfMedia){
            foreach(Media media in listOfMedia){
                yield return new MediaDTO{
                    Id =media.Id, 
                    Adult = media.Adult,
                    GenreIds = media.GenreIds,
                    MediaType = media.MediaType,
                    Language = media.Language,
                    Title = media.Title,
                    Overview = media.Overview
                };
            }
        }

        public static Media ConvertMediaDtoToMedia(MediaDTO mediaDTO){
            return new Media{
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