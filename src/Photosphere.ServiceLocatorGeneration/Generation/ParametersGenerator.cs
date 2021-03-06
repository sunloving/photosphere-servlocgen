﻿using System.Collections.Generic;
using System.Linq;
using Photosphere.ServiceLocatorGeneration.Extensions;
using static Photosphere.ServiceLocatorGeneration.Templates.ServiceLocatorTemplate;

namespace Photosphere.ServiceLocatorGeneration.Generation
{
    internal class ParametersGenerator
    {
        private readonly IReadOnlyDictionary<string, string> _parameters;

        public ParametersGenerator(ServiceLocatorConfiguration configuration)
        {
            _parameters = configuration.Parameters;
        }

        public string Generate()
            => _parameters.Select(p => ParameterExpression(p.Key, p.Value)).JoinByCommaAndSpace();
    }
}