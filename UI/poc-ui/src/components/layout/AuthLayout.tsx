import { Card } from "primereact/card";

interface AuthLayoutProps {
  children: React.ReactNode;
  title: string;
}

const AuthLayout: React.FC<AuthLayoutProps> = ({ children, title }) => {
  return (
    <div className="flex justify-content-center align-items-center min-h-screen bg-indigo-100">
      <Card title={title} className="w-full md:w-30rem">
        {children}
      </Card>
    </div>
  );
};

export default AuthLayout;
