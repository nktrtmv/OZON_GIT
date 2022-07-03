"""
==============================================================================================================
Script which handles subjects, activates processes of test generation and returns response (test / exception).
==============================================================================================================
"""

# Imports.
from app.server_api.subjects_implementation.test_generator import generate_test_by_level


async def return_test(level, subject):
    """
    Creates test instance.

    :param level: Student's grade: 9 / 10 / 11.
    :param subject: Student's subject: 'math' / 'rus'.
    :return: Generated test instance by requested subject and requested subject's level.
    """
    if subject == 'rus' or subject == 'math':
        return await generate_test_by_level(level, subject)

    return None
