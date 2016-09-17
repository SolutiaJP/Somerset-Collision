using GuessAList.DataContracts.Interfaces;
using GuessAList.Repository;

namespace GuessAList.BusinessLibrary
{
	public partial class UploadListBusinessLibrary
	{
		private readonly UploadListRepository repository;

		public UploadListBusinessLibrary(UploadListRepository repository)
		{
			this.repository = repository;
		}

		public IListDataContract UploadList(INewListDataEntryDataContract newListDataEntryDataContract, ITenantDataContract tenantDataContract)
		{
			return repository.UploadList(newListDataEntryDataContract, tenantDataContract);
		}	
	}
}

