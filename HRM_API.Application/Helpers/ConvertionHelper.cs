using System.Globalization;

namespace HRM_API.Application.Helpers
{
    public class ConversionHelper
    {
        // ---------- INT ----------
        public bool IsInt(string? input) =>
            int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);

        public int ToInt(string? input)
        {
            if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un número entero válido.");
        }

        // ---------- LONG ----------
        public bool IsLong(string? input) =>
            long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);

        public long ToLong(string? input)
        {
            if (long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un número long válido.");
        }

        // ---------- DECIMAL ----------
        public bool IsDecimal(string? input) =>
            decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out _);

        public decimal ToDecimal(string? input)
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un número decimal válido.");
        }

        // ---------- DOUBLE ----------
        public bool IsDouble(string? input) =>
            double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out _);

        public double ToDouble(string? input)
        {
            if (double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un número double válido.");
        }

        // ---------- BOOL ----------
        public bool IsBool(string? input) =>
            bool.TryParse(input, out _);

        public bool ToBool(string? input)
        {
            if (bool.TryParse(input, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un booleano válido (true/false).");
        }

        // ---------- DATETIME ----------
        public bool IsDateTime(string? input) =>
            DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        public DateTime ToDateTime(string? input)
        {
            if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es una fecha válida.");
        }

        // ---------- GUID ----------
        public bool IsGuid(string? input) =>
            Guid.TryParse(input, out _);

        public Guid ToGuid(string? input)
        {
            if (Guid.TryParse(input, out var result))
                return result;

            throw new FormatException($"El valor '{input}' no es un GUID válido.");
        }
    }
}
