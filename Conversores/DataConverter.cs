using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Conversores
{
    public static class DataConverter
    {
        public static DataDTO ToDataDTO(DateOnly dateOnly)
        {
            return new DataDTO
            {
                Year = dateOnly.Year,
                Month = dateOnly.Month,
                Day = dateOnly.Day
            };
        }
        public static DateOnly ToDateOnly(DataDTO dto)
        {
            return new DateOnly(dto.Year, dto.Month, dto.Day);
        }
    }
}
