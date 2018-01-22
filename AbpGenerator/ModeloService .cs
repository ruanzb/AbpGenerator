﻿using System.Collections.Generic;

namespace AbpGenerator
{
    public abstract class ModeloService
    {
        public static string NomePastaService { get; } = @"Application";
        public static string NomeService { get; } = @"AppService";

        public static string IService(string nameSpace, string nomeEntidade, string tipoChave)
        {
            var iService = @"
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using Abp.Authorization;
        using Abp.AutoMapper;
        using Abp.Runtime.Session;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Criar;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Deletar;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Entidade;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Obter;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Atualizar;

        namespace " + nameSpace + @"
        {
            public interface I" + nomeEntidade + NomeService + @" : IApplicationService
            { 
                Task<CriarOutput> Criar(CriarInput input);

                Task<AtualizarOutput> Atualizar(AtualizarInput input);

                Task<ObterPorIdOutput> ObterPorId(ObterPorIdInput input);

                Task Deletar(DeletarInput input);

                Task<ObterTodosOutput> ObterTodos();          
            }
        }";

            return iService;
        }

        public static string Service(string nameSpace, string nomeEntidade, string tipoChave, string nomePlural)
        {
            var service = @"
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using Abp.Authorization;
        using Abp.AutoMapper;
        using Abp.Runtime.Session;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Criar;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Deletar;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Entidade;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Obter;
        using " + nameSpace.Replace(NomeService, "Dtos") + @".Atualizar;

        namespace " + nameSpace + @"
        {
            public class " + nomeEntidade + NomeService + @" : SolutionAppServiceBase, I" + nomeEntidade + NomeService + @"
            { 
                private readonly I" + nomeEntidade + "Manager _" + nomeEntidade.ToLower() + @"Manager;
               
                public " + nomeEntidade + NomeService + @"(
                I" + nomeEntidade + "Manager " + nomeEntidade.ToLower() + @"Manager)
                {
                   _" + nomeEntidade.ToLower() + @"Manager = " + nomeEntidade.ToLower() + @"Manager;
                }

                /// <summary>
                /// Cria uma " + nomeEntidade.ToLower() + @"
                /// </summary>
                /// <param name=""input""> Dados para a criação da " + nomeEntidade.ToLower() + @" </param>
                /// <returns>
                /// Id da " + nomeEntidade.ToLower() + @" criada
                /// </returns>
                public async Task<CriarOutput> Criar(CriarInput input)
                {
                    var " + nomeEntidade.ToLower() + @" = input.MapTo<" + nomeEntidade + @">();
                    " + nomeEntidade.ToLower() + @".TenantId = AbpSession.GetTenantId();
                    var id = await _" + nomeEntidade.ToLower() + @"Manager.Criar(" + nomeEntidade.ToLower() + @");
                    return new CriarInput { Id = id };
                }

                /// <summary>
                /// Atualiza uma " + nomeEntidade.ToLower() + @"
                /// </summary>
                /// <param name=""input""> Dados para a atualizar da " + nomeEntidade.ToLower() + @" </param>
                /// <returns>
                /// " + nomeEntidade + @" atualizada
                /// </returns>
                public async Task<AtualizarOutput>" + @" Atualizar(AtualizarInput input)
                {
                    var " + nomeEntidade.ToLower() + @" = input.MapTo<" + nomeEntidade + @">();
                    " + nomeEntidade.ToLower() + @".TenantId = AbpSession.GetTenantId();
                    var " + nomeEntidade.ToLower() + @"Retornada = await _" + nomeEntidade.ToLower() + @"Manager.Atualizar(" + nomeEntidade.ToLower() + @");

                    return " + nomeEntidade.ToLower() + @"Retornada.MapTo<AtualizarOutput>();
                }

                /// <summary>
                /// Obtem a " + nomeEntidade.ToLower() + @" pelo id
                /// </summary>
                /// <param name=""input""> Id da " + nomeEntidade.ToLower() + @" </param>
                /// <returns>
                /// Dados da " + nomeEntidade.ToLower() + @"
                /// </returns>
                public async Task<ObterPorIdOutput> ObterPorId(ObterPorIdInput input)
                {
                    var " + nomeEntidade.ToLower() + @" = await _" + nomeEntidade.ToLower() + @"Manager.ObterPorId(input.Id);
                    return " + nomeEntidade.ToLower() + @".MapTo<ObterPorIdOutput>();
                }

                /// <summary>
                /// Deleta uma " + nomeEntidade.ToLower() + @"
                /// </summary>
                /// <param name=""input""> Id da " + nomeEntidade.ToLower() + @" </param>
                /// <returns>
                /// Dto vazio
                /// </returns>
                public async Task Deletar(DeletarInput input)
                {
                    await _" + nomeEntidade.ToLower() + @"Manager.Deletar(input.Id);
                    return new DeletarOutput();
                }

                /// <summary>
                /// Obter todos as " + nomeEntidade.ToLower() + @"s
                /// </summary>
                /// <returns>
                /// Lista de " + nomeEntidade.ToLower() + @"s
                /// </returns>
                public async Task<ObterTodosOutput> ObterTodos()
                {
                    var " + nomePlural.ToLower() + @" = await _" + nomeEntidade.ToLower() + @"Manager.ObterTodos();

                    return new ObterTodosOutput
                    {
                        "+ nomePlural+@" = " + nomeEntidade.ToLower() + @".MapTo<List<ItemOutput>>()
                    };
                }          
            }
        }";

             return service;
        }

        public static string Namespace(string projectName, string nomeSolucao, string nomePlural)
        {
            var name = projectName + "." + nomeSolucao + "." + nomePlural + "." + NomePastaService;
            return name;
        }
    }
}