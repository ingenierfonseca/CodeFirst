using LogicalData.Class;
using LogicalData.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalData.Repository
{
    public class CarRepository : Repository<Carrito>
    {
        public const int STATE_CREATE = 1;
        public const int STATE_EDIT = 2;
        public const int STATE_DELETE = 3;

        public List<DtoCar> GetDtoListCar(string Identificacion)
        {
            List<DtoCar> query = (from c in db.Carritos
                        join a in db.Articulos on c.IdArticulo equals a.Id
                        where c.Identificacion.Equals(Identificacion)
                        //into DetCar
                        //from a in DetCar
                        select new DtoCar()
                        {
                            Id=c.Id,
                            IdArticulo = c.IdArticulo,
                            Articulo = a.Nombre,
                            Cantidad = c.Cantidad,
                            Precio = a.Precio,
                            SubTotal = c.Cantidad * a.Precio
                        }).ToList();

            return query;
        }
        public DtoCar GetDtoCar(string Identificacion, int id)
        {
            DtoCar query = (from c in db.Carritos
                                  join a in db.Articulos on c.IdArticulo equals a.Id
                                  where c.Identificacion.Equals(Identificacion)
                                  && c.Id.Equals(id)
                                  //into DetCar
                                  //from a in DetCar
                                  select new DtoCar()
                                  {
                                      Id = c.Id,
                                      IdArticulo = c.IdArticulo,
                                      Identificacion= c.Identificacion,
                                      Articulo = a.Nombre,
                                      Cantidad = c.Cantidad,
                                      Precio = a.Precio,
                                      SubTotal = c.Cantidad * a.Precio
                                  }).FirstOrDefault();

            return query;
        }
        public bool ExistArticle(string Identificacion,int idArticle)
        {
            Carrito car = DbSet.Where(x => x.Identificacion.Equals(Identificacion) && x.IdArticulo.Equals(idArticle)).FirstOrDefault();
            if (car != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getArticleCount(string Identificacion, int idArticle)
        {
            var carCount = DbSet.Where(x => x.Identificacion.Equals(Identificacion) && x.IdArticulo.Equals(idArticle)).Select(x => x.Cantidad).FirstOrDefault();
            var articleCount = db.Articulos.Where(x => x.Id.Equals(idArticle)).Select(x => x.Cantidad).FirstOrDefault();

            return carCount + articleCount;
        }

        public void UpdateArticle(string Identificacion, int idArticle, int count, int idState)
        {
            var article = db.Articulos.Find(idArticle);

            switch (idState)
            {
                case STATE_CREATE:
                    article.Cantidad = article.Cantidad - count;
                    break;
                case STATE_EDIT:
                    var carCount = DbSet.Where(x => x.Identificacion.Equals(Identificacion) && x.IdArticulo.Equals(idArticle)).Select(x => x.Cantidad).FirstOrDefault();
                    carCount = carCount + article.Cantidad;
                    article.Cantidad = carCount - count;
                    break;
                case STATE_DELETE:
                default:
                    article.Cantidad = article.Cantidad + count;
                    break;
            }

            db.Entry(article).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
