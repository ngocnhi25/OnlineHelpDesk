namespace SharedKernel
{
    public class DataResponse<TEntity>
    {
        public List<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
