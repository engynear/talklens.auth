-- +goose Up
-- +goose StatementBegin

CREATE TABLE IF NOT EXISTS telegram_sessions (
    id UUID PRIMARY KEY,
    user_id text NOT NULL,
    session_id VARCHAR(255) NOT NULL,
    phone_number VARCHAR(20) NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL,
    last_activity_at TIMESTAMP WITH TIME ZONE,
    is_active BOOLEAN NOT NULL DEFAULT false
);

CREATE INDEX idx_telegram_sessions_user_id ON telegram_sessions(user_id);
CREATE INDEX idx_telegram_sessions_session_id ON telegram_sessions(session_id);
CREATE INDEX idx_telegram_sessions_phone ON telegram_sessions(phone_number);

-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin

DROP TABLE IF EXISTS telegram_sessions;

-- +goose StatementEnd 