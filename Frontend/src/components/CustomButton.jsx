import PropTypes from "prop-types";

export default function CustomButton({
  label,
  onClick,
  variant,
  type,
  className,
}) {
  return (
    <button
      onClick={onClick}
      variant={variant}
      type={type}
      className={className}
    >
      {label}
    </button>
  );
}

CustomButton.propTypes = {
  label: PropTypes.string.isRequired,
  variant: PropTypes.string,
  type: PropTypes.string,
};