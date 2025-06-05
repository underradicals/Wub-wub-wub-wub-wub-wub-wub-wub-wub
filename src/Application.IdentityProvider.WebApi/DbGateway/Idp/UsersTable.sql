CREATE TABLE users
(
    id               UUID PRIMARY KEY     DEFAULT gen_random_uuid(),
    email            TEXT        NOT NULL,
    normalized_email TEXT        NOT NULL,
    password         TEXT        NOT NULL,
    created          TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    modified         TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    email_confirmed  BOOLEAN     NOT NULL DEFAULT FALSE,
    refresh_token    TEXT,
    security_stamp   TEXT        NOT NULL DEFAULT gen_random_uuid()::text,
    last_login       TIMESTAMPTZ,
    xmin             xid         NOT NULL -- PostgreSQL system column for optimistic concurrency
);