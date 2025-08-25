﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {

        #region Endpoint Artistas
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            var listaDeArtistas = dal.Listar();
            if (listaDeArtistas is null)
            {
                return Results.NotFound();
            }
            var listaDeArtistaResponse = EntityListToResponseList(listaDeArtistas);
            return Results.Ok(listaDeArtistaResponse);
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(artista));

        });

        app.MapPost("/Artistas", async ([FromServices] IHostEnvironment env,
                                  [FromServices] DAL<Artista> dal, 
                                  [FromBody] ArtistaRequest artistaRequest) =>
        {
            //NOME DO ARQUIVO FOTO DE PERFIL É
            //DATA DE AGORA + NOME ARTISTA.jpeg
            var nome = artistaRequest.nome;
            var nomeArquivoImagemArtista = DateTime.Now.ToString("ddMMyyyhhss")+nome+".jpeg";

            //CAMINHO PARA SALVAR A FOTO DE PERFIL
            //PASTA wwwroot/FotosPefil
            var path = Path.Combine(
                env.ContentRootPath, "wwwroot", "FotosPerfil", nomeArquivoImagemArtista);

            //CRIAÇÃO DA IMAGEM NA PASTA
            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil!));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);


            var artista = new Artista(artistaRequest.nome, artistaRequest.bio)
            {
                //CAMINHO DA FOTO DO PERFIL SALVA NO CAMPO FotoPerfil
                FotoPerfil = $"/FotosPerfil/{nomeArquivoImagemArtista}"
            };

            dal.Adicionar(artista);
            return Results.Ok();

            /*
            OBSERVAÇÕES
            
            async ([FromServices] IHostEnvironment env
            NECESSÁRIO PARA QUE A FUNÇÃO CONSIGA ENCONTRAR O CAMINHO
            ABSOLUTO DO ARQUIVO
            ( O async É PELO USO DE await ms.CopyToAsync(fs) )
             */
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) => {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artista);
            return Results.NoContent();

        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {
            var artistaAAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
            if (artistaAAtualizar is null)
            {
                return Results.NotFound();
            }
            artistaAAtualizar.Nome = artistaRequestEdit.nome;
            artistaAAtualizar.Bio = artistaRequestEdit.bio;        
            dal.Atualizar(artistaAAtualizar);
            return Results.Ok();
        });
        #endregion
    }

    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }

  
}
