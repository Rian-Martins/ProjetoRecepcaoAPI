using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProjetoRecepcao.Identidade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoRecepcao.Identidade
{

    public class Aluno
    {

        [Key]
        public Guid AlunoId { get; set; }
        public string? Nome { get; set; }
        public string Horario { get; set; }
        public DateOnly Data { get; set; }
        public string? Professor { get; set; }
        public string DiaSemana { get; set; }


        // Propriedade de navegação para os horários do aluno
        //public ICollection<AlunoHorario> AlunoHorarios { get; set; } = new List<AlunoHorario>();

        // Propriedade de navegação para os dias da semana do aluno
        //public ICollection<AlunoDiaSemana> AlunoDiaSemanas { get; set; } = new List<AlunoDiaSemana>();


        // Construtor padrão necessário para a deserialização
        public Aluno() { }

        // Construtor que inicializa o Aluno com horários e dias da semana padrão
        public Aluno(Guid alunoId, string nome, DateOnly data, string professor, string diaSemana)
        {
            AlunoId = alunoId;
            Nome = nome;
            Data = data;
            Professor = professor;
            DiaSemana = diaSemana;

            // Adicionar horários e dias da semana padrão ao criar um novo Aluno
            AdicionarHorariosPadrao();
            //AdicionarDiasSemanaPadrao();


        }

        // Método para adicionar horários padrão ao Aluno
        private void AdicionarHorariosPadrao()
        {
            var horariosPadrao = new List<string>
            {
                "06:00 - 06:40",
                "06:40 - 07:20",
                "07:20 - 08:00",
                "08:00 - 08:40",
                "08:40 - 09:20",
                "09:20 - 10:00",
                "10:00 - 10:40",
                "10:40 - 11:20",
                "11:20 - 12:00",
                "15:30 - 16:10",
                "16:10 - 16:50",
                "16:50 - 17:30",
                "17:30 - 18:10",
                "18:10 - 18:50",
                "18:50 - 19:30",
                "19:30 - 20:10",
                "20:10 - 20:50",
                "20:50 - 21:30"
            };

            //    foreach (var horario in horariosPadrao)
            //    {
            //        AlunoHorarios.Add(new AlunoHorario
            //        {
            //            Id = Guid.NewGuid(), // Cada AlunoHorario tem um ID único                       
            //            Horario = horario,
            //            Aluno = this
            //        });
            //    }
            //}

            // Método para adicionar dias da semana padrão ao Aluno
            //private void AdicionarDiasSemanaPadrao()
            //{
            //    var diasSemanaPadrao = new List<string>
            //{
            //    "Segunda",
            //    "Terça",
            //    "Quarta",
            //    "Quinta",
            //    "Sexta"

            //};

            //    //}

            //}

            ///* Classe do Horario */
            //public class AlunoHorario
            //{
            //    public Guid Id { get; set; } = Guid.NewGuid();
            //    public string Horario { get; set; }

            //    // Relação com Aluno
            //    [JsonIgnore] // Ignora a propriedade durante a serialização JSON para evitar loops de referência
            //    public Aluno? Aluno { get; set; }
            //}


        }
    }
}
    
