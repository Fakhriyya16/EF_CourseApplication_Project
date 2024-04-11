using EF_CourseApplication_Project.Controllers;
using Service.Helpers.Constants;
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
    if(operationType > 2 || operationType < 0)
    {
        ConsoleColor.Red.ConsoleMessage("Please choose again:");
        goto IntroMenu;
    }

    switch (operationType)
    {
        case (int)IntroMenuEnums.Register:
            await userController.Register();
            goto IntroMenu;
        case (int)IntroMenuEnums.Login:
            try
            {
                await userController.Login();
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.ConsoleMessage(ex.Message);
                goto IntroMenu;
            }

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
                    await educationController.CreateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.DeleteEducation:
                    await educationController.DeleteAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.UpdateEducation:
                    await educationController.UpdateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllEducation:
                    await educationController.GetAllAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByIdEducation:
                    await educationController.GetByIdAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.SearchEducation:
                    await educationController.SearchAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllWithGroupsEducation:
                    await educationController.GetAllWithGroups();
                    goto MainMenu;
                case (int)MainMenuEnums.SortEducation:
                    await educationController.SortWithCreatedDate();
                    goto MainMenu;
                case (int)MainMenuEnums.CreateGroup:
                    await groupController.CreateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.DeleteGroup:
                    await groupController.DeleteAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.UpdateGroup:
                    await groupController.UpdateAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetAllGroups:
                    await groupController.GetAllAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByIdGroup:
                    await groupController.GetByIdAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.SearchGroup:
                    await groupController.SearchAsync();
                    goto MainMenu;
                case (int)MainMenuEnums.FilterByEducationName:
                    await groupController.FilterByEducationName();
                    goto MainMenu;
                case (int)MainMenuEnums.GetByEducationId:
                    await groupController.GetAllByEducationId();
                    goto MainMenu;
                case (int)MainMenuEnums.SortByCapacity:
                    await groupController.SortByCapacity();
                    goto MainMenu;
            }
        break;
    }
}