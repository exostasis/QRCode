﻿using System;

namespace Exostasis.QR.Polynomial
{ 
    public class ConstantTerm
    {
        public int _constant { get; }
        public Variable _variable { get; }

        public ConstantTerm (int constant)
        {
            _constant = constant;
        }

        public ConstantTerm (int constant, string variable, int exponent) : this(constant)
        {
            _variable = new Variable(variable, exponent);
        }

        public ConstantTerm (int constant, Variable variable) : this(constant)
        {
            _variable = variable;
        }

        public Byte ToByte ()
        {
            return Convert.ToByte(_constant);
        }
    }
}
