﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Chatbot
{
    public class QuestionsResponse
    {
        public List<string> Questions { get; set; } = (["Como recuperar contrasña?",
            "Por que puedo ser baneado?",
            "Como se usan mis datos?",
            "Que tipos de recursos puedo encontrar en la biblioteca?",
            "Como se usan mis datos?",
            "Que puedo vender en la tienda?"
            ]);


    }
}
