﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapReservas.Domain.Interfaces.Services.SMS
{
    public interface ISmsService
    {
        void EnviarMensagem(string telefone, string mensagem);
    }
}
