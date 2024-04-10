using EF_CourseApplication_Project.Controllers;
using Service.Helpers.Enums;
using Service.Helpers.Extensions;

EducationController educationController = new EducationController();
GroupController groupController = new GroupController();
UserController userController = new UserController();

void IntroMenu()
{
    ConsoleColor.Magenta.ConsoleMessage("Press\n1. Register\n2. Login");
}
void MainMenu()
{
    ConsoleColor.Magenta.ConsoleMessage("Education Operations\n1. Create Education\n2.Delete Education\n3.Update Education\n4.Get All Educations\n5.Get Education By Id\n6.Search Education\n7.Get All With Groups\n8.Sort By Created Date");
    ConsoleColor.Magenta.ConsoleMessage("Group Operations\n9. Create group\n10. Delete Group\n11. Update Group\n12. Get All Groups\n13. Get Group By Id\n14. Search groups\n15. Filter By Education Name\n16. Get All Groups By Education Id\n17. Sort Groups By Capacity");
}

IntroMenu: IntroMenu();
while (true)
{
OperationType: string operationTypeStr = Console.ReadLine();
    int operationType;
    bool isCorrect = int.TryParse(operationTypeStr, out operationType);
    if (!isCorrect)
    {
        ConsoleColor.Red.ConsoleMessage("Wront format");
        goto OperationType;
    }

    switch (operationType)
    {
        case (int)IntroMenuEnums.Register:
            userController.Register();
            goto IntroMenu;
        case (int)IntroMenuEnums.Login:
            userController.Login(); 
        MainMenu: MainMenu();
        MainOperationType: string mainOperationTypeStr = Console.ReadLine();
            int mainOperationType;
            isCorrect = int.TryParse(mainOperationTypeStr, out mainOperationType);
            if (!isCorrect)
            {
                ConsoleColor.Red.ConsoleMessage("Wront format");
                goto MainOperationType;
            }

            switch (mainOperationType)
            {
                case (int)MainMenuEnums.CreateEducation:
                    educationController.CreateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.DeleteEducation:
                    educationController.DeleteAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.UpdateEducation:
                    educationController.UpdateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllEducation:
                    educationController.GetAllAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByIdEducation:
                    educationController.GetByIdAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.SearchEducation:
                    educationController.SearchAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllWithGroupsEducation:
                    educationController.GetAllWithGroups();
                    goto MainMenu;
                case (int)MainMenuEnums.SortEducation:
                    educationController.SortWithCreatedDate();
                    goto MainMenu;
                case (int)MainMenuEnums.CreateGroup:
                    groupController.CreateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.DeleteGroup:
                    groupController.DeleteAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.UpdateGroup:
                    groupController.UpdateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllGroups:
                    groupController.GetAllAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByIdGroup:
                    groupController.GetByIdAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.SearchGroup:
                    groupController.SearchAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.FilterByEducationName:
                    groupController.FilterByEducationName();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByEducationId:
                    groupController.GetAllByEducationId();
                    goto MainMenu;
                case (int)MainMenuEnums.SortByCapacity:
                    groupController.SortByCapacity();
                    goto MainMenu;
            }
        break;
    }
}