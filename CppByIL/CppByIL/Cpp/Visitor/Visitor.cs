using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{

    public abstract class Visitor
    {

        #region precessor
        public abstract void VisitIncludePrecessor(Include node);
        public abstract void VisitPragmaPrecessor(Pragma node);
        #endregion

        #region type declaration
        public abstract void VisitNamespaceDeclaration(NamespaceDeclaration node);
        public abstract void VisitClassDeclaration(ClassDeclaration node);
        public abstract void VisitMethodDefinition(MethodDefinition node);
        public abstract void VisitMethodDeclaration(MethodDeclaration node);
        public abstract void VisitMethodParameter(MethodParameter node);
        public abstract void VisitTypeReference(CppTypeReference node);
        public abstract void VisitMethodBodyDefinition(MethodBodyDefinition node);
        #endregion

        #region Statement
        public abstract void VisitExpressionStatement(ExpressionStatement node);
        public abstract void VisitReturnStatement(ReturnStatement node);
        public abstract void VisitLocalVariableDeclareStatement(LocalVariableDeclareStatement node);
        #endregion

        #region Expression
        public abstract void VisitBinaryExpression(RightValueBinaryExpression node);
        public abstract void VisitLocalVariableReadExpression(LocalVariableReadExpression node);
        public abstract void VisitLocalVariableWriteExpression(LocalVariableWriteExpression node);
        public abstract void VisitAssignmentExpression(AssignmentExpression node);
        #endregion

        #region IL
        public abstract void VisitILStatement(ILStatement node);
        public abstract void VisitILInstruction(ILInstruction node);
        #endregion
    }

}
