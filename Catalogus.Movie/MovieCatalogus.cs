using Database.Entities.Interfaces;
using DataTransferObjectLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catalogus.Movie
{
    public class MovieCatalogus : IEnumerable<MovieDto>
    {
        private IRepositoryContext ctx;

        public MovieCatalogus( IRepositoryContext _ctx )
        {
            ctx = _ctx;
        }

        public IEnumerator<MovieDto> GetEnumerator()
        {
            return ctx.Movies.Select( o => DTOMapper.ToMovieDTO(o) ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
