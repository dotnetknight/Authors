using Authors.CQRS.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace Authors.Models.Commands
{
    public class UpdateCourseForAuthorCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public JsonPatchDocument<UpdateCourseForAuthorCommand> JsonPatchDocument { get; set; }

        public UpdateCourseForAuthorCommand(JsonPatchDocument<UpdateCourseForAuthorCommand> jsonPatchDocument)
        {
            JsonPatchDocument = jsonPatchDocument;
        }
    }
}
