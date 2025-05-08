using TmoTask.Interfaces;

namespace TmoTask.Services
{
    public class BranchService: IBranchService
    {
        private readonly IDataHandler _dataHandler;
        public BranchService(IDataHandler dataHandler) {
            _dataHandler = dataHandler;
        }

        public async Task<IEnumerable<string>> GetBranchesAsync()
        {
            var branches = await _dataHandler.GetBranchesAsync();

            return branches.OrderBy(x => x);
        }


    }
}
