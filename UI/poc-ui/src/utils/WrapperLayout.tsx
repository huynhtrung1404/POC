import AuthLayout from "../components/layout/AuthLayout";
import MainLayout from "../components/layout/MainLayout";
import ProtectedRoute from "../routes/ProtectedRoute";
import PublicRoute from "../routes/PublicRoute";

interface WrapperProp {
  title?: string;
  children: React.ReactNode;
}

const AuthLayoutWrapper: React.FC<WrapperProp> = ({ children, title }) => {
  return (
    <PublicRoute>
      <AuthLayout title={title ?? ""}>{children}</AuthLayout>
    </PublicRoute>
  );
};

const MainLayoutWrapper: React.FC<WrapperProp> = ({ children }) => {
  return (
    <ProtectedRoute>
      <MainLayout>{children}</MainLayout>
    </ProtectedRoute>
  );
};

export { AuthLayoutWrapper, MainLayoutWrapper };
