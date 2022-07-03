"""
================================================================================================
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
Script was used for reformatting mistakes during data collecting. It is commented because can be
used in future!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
================================================================================================
"""


# import json
# from sdamgia import SdamGIA
# import tests_info

# sdamgia_instance = SdamGIA()
#
# final_list = []
# tasks_with_restrictions = ("Правописание приставок", "Грамматическая основа предложения", "Правописание НЕ и НИ",
#                            "Простое осложненное предложение")
# tasks_with_solution = ("Грамматическая основа предложения", "Простое осложненное предложение")
#
#
# def get_tasks_data():
#     subject = 'rus_lib'
#     with open('../server_api/subject_data/rus_lib/rus_data.txt', 'r') as read_file:
#         data_list = json.load(read_file)
#
#     for item in data_list:
#         for theme, ids_list in item.items():
#             tmp_list = []
#             for subject_id in ids_list:
#                 response = sdamgia_instance.get_problem_by_id(subject, subject_id)
#
#                 condition = response.get('condition').get('text').replace(' ', ' ').replace(' ', ' ').strip()
#                 task_text_data = response.get('solution').get('text').replace(' ', ' ').replace(' ', ' ').strip()
#                 answer = response.get('answer').replace(' ', ' ').replace(' ', ' ').strip()
#                 url = response.get('url').strip()
#
#                 tmp_list.append(
#                     {
#                         'Condition': condition,
#                         'Task text data': task_text_data,
#                         'Answer': answer,
#                         'Url': url
#                     }
#                 )
#
#             final_list.append(
#                 {
#                     theme: tmp_list
#                 }
#             )
#             print(f"[INFO] Theme: {theme}  --->  Result: success!")
#
#     with open('../server_api/subject_data/rus_lib/rus_conditions_and_answers.txt', 'w') as write_file:
#         json.dump(final_list, write_file, indent=4, ensure_ascii=False)
#
#
# def correct_file():
#     final_data = []
#     with open('../server_api/subject_data/rus_lib/rus_conditions_and_answers.txt', 'r') as read_file, open(
#             '../server_api/subject_data/rus_lib/rus_task_bank.txt', 'w') as write_file:
#         data_list = json.load(read_file)
#         for data in data_list:
#             for theme, tasks_list in data.items():
#                 if theme in tests_info.rus_themes_11_level.keys():
#                     final_data.append(
#                         {
#                             theme: tasks_list
#                         }
#                     )
#                     print(f"[INFO] Theme: {theme}  --->  Result: success!")
#         json.dump(final_data, write_file, indent=4, ensure_ascii=False)
#
#
# def get_final_rus_tasks_bank():
#     final_data = []
#     with open('../server_api/subject_data/rus_lib/rus_task_bank.txt', 'r') as read_file, open(
#             '../server_api/subject_data/rus_lib/rus_final_tasks_bank.txt', 'w') as write_file:
#         current_data = json.load(read_file)
#         for data in current_data:
#             for theme, tasks_list in data.items():
#                 tmp_arr = []
#                 if theme not in tasks_with_restrictions and theme not in tasks_with_solution:
#                     for item in tasks_list:
#                         condition = item.get('Condition')
#                         answer = item.get('Answer')
#
#                         tmp_arr.append(
#                             {
#                                 "Condition": condition,
#                                 "Answer": answer
#                             }
#                         )
#                 final_data.append(
#                     {
#                         tests_info.rus_themes_11_level[theme]: tmp_arr
#                     }
#                 )
#
#         for data in current_data:
#             for theme, tasks_list in data.items():
#                 if theme in tasks_with_restrictions or theme in tasks_with_solution:
#                     final_data.append(
#                         {
#                             tests_info.rus_themes_11_level[theme]: tasks_list
#                         }
#                     )
#
#         json.dump(final_data, write_file, indent=4, ensure_ascii=False)


# def main():
#     get_tasks_data()
#     correct_file()
#     get_final_rus_tasks_bank()
#
#
#
# if __name__ == '__main__':
#     main()
