"""
=========================
Script-generator of test.
=========================
"""


# Imports.
import json
import app.server_api.subjects_implementation.math_test_themes_info as math_test_themes_info
import app.server_api.subjects_implementation.rus_test_themes_info as rus_test_themes_info

from random import randint, shuffle


def get_math_themes_dictionary(level: int):
    """
    Handles different types of level.

    :param level: Student's grade: 9 / 10 / 11.
    :return: Dictionary with themes which will be used for current grade during test variant generation or None in case of incorrect level value.
    """
    if level == 9:
        return math_test_themes_info.math_themes_9_level
    elif level == 10:
        return math_test_themes_info.math_themes_10_level
    elif level == 11:
        return math_test_themes_info.math_themes_11_level

    return None


def get_rus_themes_dictionary(level: int):
    """
    Handles different types of level.

    :param level: Student's grade: 9 / 10 / 11.
    :return: Dictionary with themes which will be used for current grade during test variant generation or None in case of incorrect level value.
    """
    if level == 9:
        return rus_test_themes_info.rus_themes_9_level
    elif level == 10:
        return rus_test_themes_info.rus_themes_10_level
    elif level == 11:
        return rus_test_themes_info.rus_themes_11_level

    return None


def get_tasks_pull_from_file(read_file_path: str) -> list:
    """
    Gets data from tasks file (pull of tasks).

    :param read_file_path: File with pull of tasks.
    :return: List of tasks from pull in json format.
    """
    with open(read_file_path, 'r') as read_file:
        data_list = json.load(read_file)

    return data_list


def create_random_test(data_list: list, using_themes_dict: dict) -> list:
    """
    Creates instance of test.

    :param data_list: List of tasks from pull in json format.
    :param using_themes_dict: Dictionary with themes which will be used for current grade.
    :return: List with test instance (theme - condition - answer  ->  for each task).
    """
    test = []

    for item in data_list:
        for theme, tasks_list in item.items():
            # Check theme availability in themes according to the student's grade.
            if theme in using_themes_dict.values():

                # Generate random task from pull for current theme.
                task_num = randint(0, len(tasks_list) - 1)

                # Append all data about task to final list.
                test.append(
                    {
                        "Theme": theme,
                        "Task":
                            {
                                "Condition": tasks_list[task_num].get('Condition'),
                                "Answer": tasks_list[task_num].get('Answer')
                            }
                    }
                )

    return test


async def generate_test_by_level(level: int, subject: str):
    """
    Generator of final response with a ready test variant.

    :param level: Student's grade: 9 / 10 / 11.
    :param subject: Student's subject: 'math' / 'rus'.
    :return: Final response (generated test) in json format or returns None.
    """
    # Definition of themes dictionary depending on level of subject which came from request body.
    if subject == 'math':
        using_themes_dict = get_math_themes_dictionary(level)
    elif subject == 'rus':
        using_themes_dict = get_rus_themes_dictionary(level)
    else:
        using_themes_dict = None

    # Check for successful themes dictionary generation.
    if using_themes_dict is None:
        return None

    # Getting data from tasks bank to list.
    if subject == 'rus':
        read_file_path = "app/server_api/subject_data/rus_lib/rus_final_tasks_bank.txt"
    elif subject == 'math':
        read_file_path = "app/server_api/subject_data/math_lib/math_final_tasks_bank.txt"
    else:
        return None

    data_list = get_tasks_pull_from_file(read_file_path)

    # Creating test variant.
    test = create_random_test(data_list, using_themes_dict)

    # Shuffling test tasks.
    shuffle(test)

    # Forming final test tasks list for client api response.
    test_tasks = [test[i].get('Task').get('Condition') for i in range(len(test))]

    # Forming final test answers list for client api response.
    test_answers = [test[i].get('Task').get('Answer') for i in range(len(test))]

    # Forming final test themes list for client api response.
    test_themes = [test[i].get('Theme') for i in range(len(test))]

    return [
        {
            "testTasks": test_tasks,
            "testAnswers": test_answers,
            "testThemes": test_themes
        }
    ] if len(test_tasks) == len(test_answers) == len(test_themes) else None
