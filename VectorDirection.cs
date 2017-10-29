namespace Matrics
{
    /// <summary>
    /// The type of directions that a 1D matrix can be in.
    /// Vertical and Column are synonyms, and Horizontal and Row are synonyms.
    /// </summary>
    public enum VectorDirection{
        Vertical,
        Column = Vertical,
        Horizontal,
        Row = Horizontal
    }
}