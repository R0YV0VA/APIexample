using ExtensionsCRUD.Domain.Models;

namespace ExtensionsCRUD.Application.Services
{
    public interface IExtensionService
    {
        ExtensionModel createExtension(string name, string description, string version, string conString);
        ExtensionModel getExtensionById(int id, string conString);
        List<ExtensionModel> getExtensions(string conString);
        ExtensionModel putExtension(int id, string name, string description, string version, string conString);
        ExtensionModel deleteExtensionById(int id, string conString);
    }
}
