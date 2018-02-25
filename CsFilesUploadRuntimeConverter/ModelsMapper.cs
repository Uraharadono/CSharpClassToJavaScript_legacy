using System.Collections.Generic;
using System.Linq;
using Utility.Extensions;

namespace CsFilesUploadRuntimeConverter
{
    public static class ModelsMapper
    {
        public static JSBuilderModel GetBuilderModel(List<FileLinesOverviewModel> listOfProperties)
        {
            JSBuilderModel retModel = new JSBuilderModel
            {
                FileClasses = new List<FileClassModel>()
            };

            // Get all unique class names
            var uniqeClassNamesList = listOfProperties
                .DistinctBy(s => s.ClassName)
                .Select(s => s.ClassName)
                .ToList();

            foreach (var cName in uniqeClassNamesList)
            {
                // Insert first class name
                var tempFileClassModel = new FileClassModel
                {
                    ClassName = cName,
                    FileProperties = new List<FilePropertyModel>()
                };

                // Find their properties
                var listOfClassProperties = listOfProperties
                    .Where(s => s.ClassName.ToLower().Trim().Contains(cName.ToLower().Trim())).ToList();

                // Then insert their properties
                foreach (var property in listOfClassProperties)
                {
                    FilePropertyModel tempFilePropertyModel = new FilePropertyModel
                    {
                        PropertyType = property.LineType.PropertyType,
                        IsArray = property.LineType.IsArray,
                        PropertyName = property.PropertyName,
                        PropertyTypeName = property.LineType.PropertyTypeName
                    };
                    tempFileClassModel.FileProperties.Add(tempFilePropertyModel);
                }

                retModel.FileClasses.Add(tempFileClassModel);
            }

            return retModel;
        }
    }
}
