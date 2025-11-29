import { Card } from "primereact/card";

interface AuthLayoutProps {
  children: React.ReactNode;
  title?: string;
}

const AuthLayout: React.FC<AuthLayoutProps> = ({ children, title }) => {
  return (
    <div className="flex flex-1 justify-content-center align-items-center surface-ground p-4">
      <Card title={title} className="w-full md:w-30rem">
        {children}
      </Card>
    </div>
  );
};

export default AuthLayout;
