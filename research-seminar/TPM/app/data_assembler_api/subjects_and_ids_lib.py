"""
================================================================================================
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
Script which collects data from external SdamGIA Api (https://github.com/anijackich/sdamgia-api)
and executes ONLY MANUALLY IF NEEDED BUT NOT RECOMMENDED!!!!!!!!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
DO NOT UNCOMMENT SCRIPT AND EXECUTE!
================================================================================================
"""
#
#
# # Imports.
# import json
#
# from sdamgia import SdamGIA
#
# # Instance of class sdamgia (which regulates processes with external api).
# sdamgia_instance = SdamGIA()
#
# # Paths to files for different subjects (rus and math are currently here).
# file_paths = {
#     'math_lib': {
#         'math_data': '../server_api/subject_data/math_lib/math_final_tasks_bank.txt',
#         'math_statistics': '../server_api/subject_data/math_lib/math_tasks_statistics.txt'
#     },
#     'rus_lib': {
#             'rus_data': '../server_api/subject_data/rus_lib/rus_final_tasks_bank.txt',
#             'rus_statistics': '../server_api/subject_data/rus_lib/rus_tasks_statistics.txt'
#         }
# }
#
#
# def get_task_catalog_from_sdamgia_api(subject: str, file_path: str) -> None:
#     """
#     Gets data from external api and writes it to json file.
#
#     :param subject: Student's subject: 'math' / 'rus'.
#     :param file_path: Path to file which we will be used for writing data.
#     :return: None.
#     """
#     data_json = []
#     topic_names = []
#     with open(file_path, 'w') as file:
#         for topic in sdamgia_instance.get_catalog(subject):
#             # Get name of theme and tasks for it.
#             topic_name = topic.get('topic_name').replace('­', '').replace('Задания Л', 'Другое').strip()
#             topic_tasks = sdamgia_instance.search(subject, topic_name)
#
#             # Avoid tasks with empty response from external api.
#             if len(topic_tasks) == 0 or topic_name in topic_names:
#                 print(f"[INFO] Topic: {topic_name}  --->  Result: no tasks!")
#                 continue
#
#             # Add data to final list and print success result to console for comfort.
#             data = {
#                 topic_name: topic_tasks
#             }
#
#             topic_names.append(topic_name)
#             data_json.append(data)
#             print(f"[INFO] Topic: {topic_name}  --->  Result: success!")
#
#         json.dump(data_json, file, indent=4, ensure_ascii=False)
#
#     return None
#
#
# def get_tasks_statistics_from_data_file(file_read_path: str, file_write_path: str) -> None:
#     """
#     Collects statistics about current subject (informs theme -> amount of tasks in tasks pull).
#
#     :param file_read_path: Path to file which we will be used for reading data.
#     :param file_write_path: Path to file which we will be used for writing data.
#     """
#     with open(file_read_path, 'r') as read_file, open(file_write_path, 'w') as write_file:
#         current_data = json.load(read_file)
#
#         final_data = []
#         for item in current_data:
#             for theme, tasks_list in item.items():
#                 final_data.append(f"{theme}: {len(tasks_list)}\n")
#
#         write_file.writelines(final_data)
#
#     return None
#
#
# def main():
#     """
#     Executes all funcs in script and collects data from external SdamGIA Api.
#     """
#     for subject in file_paths.keys():
#         file_read_path = file_paths.get(subject).get(f'{subject}_data')
#         file_write_path = file_paths.get(subject).get(f'{subject}_statistics')
#         get_task_catalog_from_sdamgia_api(subject, file_read_path)
#         get_tasks_statistics_from_data_file(file_read_path, file_write_path)
#
#
# if __name__ == '__main__':
#     main()
