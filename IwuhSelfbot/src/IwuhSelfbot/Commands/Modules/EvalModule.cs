﻿using Discord;
using Discord.Commands;
using IwuhSelfbot.Commands.Entities;
using IwuhSelfbot.Commands.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IwuhSelfbot.Commands.Modules
{
    public class EvalModule : ModuleBase
    {
        private EvalService _evaluator;

        public EvalModule(EvalService eval)
        {
            _evaluator = eval;
        }

        [Command("eval")]
        [Summary("[Advanced] Evaluate a C# expression using the Roslyn Scripting API.")]
        public async Task EvaluateExpression([Remainder, Summary("The C# expression to evaluate.")] string expr)
        {
            EvalResult result = await _evaluator.EvaluateAsync(expr, Context);

            if (result.IsSuccessful)
            {
                await ReplyAsync($"Input:\n{Format.Code(expr)}\nOutput:\n{Format.Code(result.Output)}");
            }
            else
            {
                await ReplyAsync($"Exception:\n{Format.Code(result.Output)}");
            }
        }
    }
}
