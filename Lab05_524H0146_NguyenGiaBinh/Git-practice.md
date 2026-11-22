# Thực hành nhóm hai người
1. A tạo repo WinForms trên GitHub, thêm .gitignore Visual Studio, mời B trong Settings → Collaborators. B nhận lời bằng tài khoản của mình.
2. A mở Desktop/Desktop.csproj, chạy ứng dụng, commit các tệp nguồn, School.sql và App.config. Trong Git Changes chọn Push; B clone repo, mở project và chạy cùng script SQL ở máy B.
3. B tạo nhánh feature/customer-list, thêm Label Student Name và TextBox txtStudentName trên form sinh viên, commit và push nhánh. A tạo nhánh khác để thêm mô tả thao tác Refresh. Mở pull request, kiểm tra diff rồi merge.
4. Trước khi lấy bản mới, A commit công việc đang làm hoặc git stash push -m "local edits". Chạy git pull --rebase, rồi git stash pop nếu đã stash. Nếu cùng sửa một dòng, chọn nội dung cuối cùng, xóa ký hiệu conflict, git add và hoàn tất rebase.
5. Chụp lời mời, danh sách branch, commit của mỗi người, PR và kết quả chạy từ bản clone. Báo cáo cá nhân ghi tài khoản và URL có thật.
