﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Chatbot;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IChatbotService
    {
        Task<ChatbotResponse> getChatbotReponseAsync(string prompt);
    }
}
