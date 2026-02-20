using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegrador.LogicaAplication.CasosDeUso
{
    public class AgregrComentarioCasoDeUso : IAgregarComentario
    {

        private readonly IComentarioRepositorio _comentarioRepo;

        public AgregrComentarioCasoDeUso(IComentarioRepositorio comentarioRepo)
        {
            _comentarioRepo = comentarioRepo;
        }

        public void Ejecutar(AgregarComentarioDto dto, Cliente c, Artesano a)
        {

            if(a == null)
            {
                throw new Exception("Artesano inválido");
            }
            if (c == null)
            {
                throw new Exception("cliente inválido");
            }

            var Comentario = new Comentario
            {
                contenido = dto.contenido,
                clienteId = c.id,
                artesanoId = a.id,
                //cliente = c,
                //artesano = a
            };

            //a.comentarios.Add(Comentario);
            _comentarioRepo.Agregar(Comentario);
        }
    }
}
