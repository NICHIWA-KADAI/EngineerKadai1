Public Class Message
    Private Const CON_MSG As String = "MSG"
    Private Const CON_MSG_ERR As String = "1"
    Private Const CON_MSG_EXC As String = "2"
    Private Const CON_MSG_CON As String = "3"
    Private Const CON_MSG_INF As String = "4"

    Public Shared Function ShowMessage(ByVal strMsgType As String, Optional strAddMsg As String = Nothing) As MsgBoxResult

        Dim objMsgButton As MessageBoxButtons    ' メッセージボタン
        Dim objMsgIcon As MessageBoxIcon         'メッセージアイコン


        'ボタン、アイコン取得
        Select Case strMsgType.Substring(0, 1)

            'エラー
            Case CON_MSG_ERR

                objMsgButton = MessageBoxButtons.OK
                objMsgIcon = MessageBoxIcon.Error

            '警告
            Case CON_MSG_EXC

                objMsgButton = MessageBoxButtons.OK
                objMsgIcon = MessageBoxIcon.Exclamation

           '確認
            Case CON_MSG_CON

                objMsgButton = MessageBoxButtons.OKCancel
                objMsgIcon = MessageBoxIcon.Question

            '情報
            Case CON_MSG_INF

                objMsgButton = MessageBoxButtons.OK
                objMsgIcon = MessageBoxIcon.Information

        End Select

        'メッセージ表示
        ShowMessage = MessageBox.Show(My.Resources.ResourceManager.GetString(CON_MSG + strMsgType) & strAddMsg,
                                      My.Resources.title,
                                      objMsgButton,
                                      objMsgIcon)

    End Function
End Class
