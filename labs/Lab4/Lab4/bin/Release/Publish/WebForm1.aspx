<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Lab4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title></title>
    <link rel="stylesheet" href="StyleSheet1.css" />
  </head>
  <body>
    <form runat="server">
      <div class="main">
        <div class="main__right">
            <h2>Ссылки</h2>
            <ul>
              <li><asp:LinkButton ID="LinkList" OnClick="LinkList_Click" runat="server">Список</asp:LinkButton></li>
              <li><asp:LinkButton ID="LinkAdd" OnClick="LinkAdd_Click" runat="server">Добавить</asp:LinkButton></li>
            </ul>
        </div>
        <div class="main__container">
          <div class="table" id="table" runat="server">
            <div class="table__controllers">
              <div class="show">
                <h3>Показать</h3>
                <input type="date" name="dateFrom" id="dateFrom" runat="server" />
                <span>|</span>
                <input type="date" name="dateEnd" id="dateEnd"  runat="server" />
                <asp:Button ID="btnShowByDate" runat="server" class="btn" Text="Показать" OnClick="btnShowByDate_Click" />
              </div>
              <div class="delete">
                <h3>Удалить</h3>
                <asp:Button ID="btnDelete" runat="server" class="btn" Text="Удалить" OnClick="btnDelete_Click" />
              </div>
            </div>
            <div class="table__content">
              <asp:GridView ID="GridView1" runat="server">
                  <Columns runat="server">
                        <asp:TemplateField HeaderText="X">
                        <ItemTemplate>
                            <asp:CheckBox ID="CbSel" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
          </div>
          <div class="addPanel" id="addPanel" runat="server">
            <div class="add__inputs">
              <h2>Добавить сервис</h2>
              <label for="selectAuto">Выбрать Автомобиль</label>
              <asp:DropDownList name="selectAuto" id="selectAuto" runat="server" required="true">
              </asp:DropDownList>
              <label for="selectWork">Выбрать работу</label>
              <asp:DropDownList name="selectWork" id="selectWork" runat="server" required="true">
              </asp:DropDownList>
              <label for="workTime">Указать время работы</label>
              <input type="text" name="workTime" id="workTime" runat="server" placeholder="00:00:00" required="true" />
              <label for="workDate">Указать дату работы</label>
              <input type="date" name="workDate" id="workDate" runat="server" required="true" />
              <asp:Button ID="Addbutton" runat="server" class="btn" Text="Добавить" OnClick="Addbutton_Click" />
            </div>
          </div>
        </div>
      </div>
    </form>
  </body>
</html>
